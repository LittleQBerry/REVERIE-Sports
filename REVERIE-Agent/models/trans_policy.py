import torch.nn as nn
import torch
from utils.math import *

class PatchEmbedding(nn.Module):
    def __init__(self, img_size=224, patch_size=16, in_channels=3, embed_dim=768):
        super().__init__()
        self.num_patches = (img_size // patch_size) ** 2
        self.patch_size = patch_size
        self.proj = nn.Conv2d(in_channels, embed_dim, kernel_size=patch_size, stride=patch_size)
        self.cls_token = nn.Parameter(torch.zeros(1, 1, embed_dim))
        self.pos_embed = nn.Parameter(torch.zeros(1, self.num_patches + 1, embed_dim))
        self.dropout = nn.Dropout(0.1)

    def forward(self, x):
        B = x.shape[0]
        x = self.proj(x).flatten(2).transpose(1, 2)  # [B, num_patches, embed_dim]
        cls_tokens = self.cls_token.expand(B, -1, -1)  # [B, 1, embed_dim]
        x = torch.cat((cls_tokens, x), dim=1)  # [B, num_patches + 1, embed_dim]
        x = x + self.pos_embed
        x = self.dropout(x)
        return x

class TransformerEncoderLayer(nn.Module):
    def __init__(self, embed_dim, num_heads, mlp_ratio=4.0, dropout=0.1):
        super().__init__()
        self.norm1 = nn.LayerNorm(embed_dim)
        self.attn = nn.MultiheadAttention(embed_dim, num_heads, dropout=dropout)
        self.norm2 = nn.LayerNorm(embed_dim)
        self.mlp = nn.Sequential(
            nn.Linear(embed_dim, int(embed_dim * mlp_ratio)),
            nn.GELU(),
            nn.Linear(int(embed_dim * mlp_ratio), embed_dim),
            nn.Dropout(dropout)
        )

    def forward(self, x):
        x = x + self.attn(self.norm1(x), self.norm1(x), self.norm1(x))[0]
        x = x + self.mlp(self.norm2(x))
        return x

class VisionTransformer(nn.Module):
    def __init__(self, img_size=224, patch_size=16, in_channels=3, embed_dim=768, depth=12, num_heads=12, mlp_ratio=4.0, dropout=0.1):
        super().__init__()
        self.patch_embed = PatchEmbedding(img_size, patch_size, in_channels, embed_dim)
        self.encoder = nn.Sequential(
            *[TransformerEncoderLayer(embed_dim, num_heads, mlp_ratio, dropout) for _ in range(depth)]
        )
        self.norm = nn.LayerNorm(embed_dim)
        self.feature_proj = nn.Linear(embed_dim, 256)
    def forward(self, x):
        x = self.patch_embed(x)  # [B, num_patches + 1, embed_dim]
        x = x.transpose(0, 1)  # [num_patches + 1, B, embed_dim]
        x = self.encoder(x)  # [num_patches + 1, B, embed_dim]
        x = x[0]  # [B, embed_dim]
        x = self.norm(x)
        x= self.feature_proj(x)
        return x


class TransformerBlock(nn.Module):
    def __init__(self, dim, num_heads, mlp_ratio=4.0, dropout=0.1):
        super().__init__()
        self.attn = nn.MultiheadAttention(embed_dim=dim, num_heads=num_heads, dropout=dropout)
        self.norm1 = nn.LayerNorm(dim)
        self.norm2 = nn.LayerNorm(dim)
        self.mlp = nn.Sequential(
            nn.Linear(dim, int(dim * mlp_ratio)),
            nn.GELU(),
            nn.Linear(int(dim * mlp_ratio), dim),
            nn.Dropout(dropout)
        )

    def forward(self, x):
        # Self-attention
        attn_output, _ = self.attn(x, x, x)
        x = x + attn_output
        x = self.norm1(x)
        
        # Feed-forward network
        ffn_output = self.mlp(x)
        x = x + ffn_output
        x = self.norm2(x)
        
        return x

class Policy(nn.Module):
    def __init__(self, state_dim, action_dim, transformer_dim=128, activation='tanh', num_layers=5, num_heads=4, dropout=0.1, log_std=0):
        super().__init__()
        self.is_disc_action = False
        
        self.input_proj = nn.Linear(256, transformer_dim)
        if activation == 'tanh':
            self.activation = torch.tanh
        elif activation == 'relu':
            self.activation = torch.relu
        elif activation == 'sigmoid':
            self.activation = torch.sigmoid
        
        #image
        self.image_extractor = VisionTransformer(
            image_size=224,
            patch_size=16,
            input_channels=3,
            embed_dim=768,
            num_heads=num_heads,
            depth=2,
            dropout=dropout
        )
        #sequence
        self.sequence_extractor = nn.Sequential(
            nn.Linear(9, 64),
            nn.ReLU(),
            nn.Linear(64, 64)
        )
        self.image_proj = nn.Linear(128, transformer_dim // 2)
        self.seq_proj = nn.Linear(64, transformer_dim // 2)
        self.position_embedding = nn.Parameter(torch.randn(1, 2, transformer_dim))
        # Transformer blocks
        self.transformer_layers = nn.ModuleList([
            TransformerBlock(dim=transformer_dim, num_heads=num_heads, dropout=dropout)
            for _ in range(num_layers)
        ])
        
        #Output
        self.action_mean = nn.Linear(transformer_dim, action_dim)
        self.action_mean.weight.data.mul_(0.1)
        self.action_mean.bias.data.mul_(0.0)

        self.action_log_std = nn.Parameter(torch.ones(1, action_dim) * log_std)

    def forward(self, x):
        img = x[:,0]
        seq= x[:,1]
        image_feat = self.image_extractor(img)
        image_feat = self.image_proj(image_feat)
        seq_feat =self.sequence_extractor(seq)
        seq_feat =self.seq_proj(seq_feat)
        combined_feat = torch.cat((image_feat, seq_feat), dim=1)
        combined_feat = combined_feat.unsqueeze(1)
        x = combined_feat + self.position_embedding[:, :1, :]

        x = x.permute(1,0,2)
        for layer in self.transformer_layers:
            x = layer(x)
        x = x[0]
        x = self.activation(x)
        
        # Compute action mean and std
        action_mean = self.action_mean(x)
        action_log_std = self.action_log_std.expand_as(action_mean)
        action_std = torch.exp(action_log_std)
        
        return action_mean, action_log_std, action_std

    def select_action(self, x):
        action_mean, _, action_std = self.forward(x)
        action = torch.normal(action_mean, action_std)
        return action

    def get_kl(self, x):
        mean1, log_std1, std1 = self.forward(x)

        mean0 = mean1.detach()
        log_std0 = log_std1.detach()
        std0 = std1.detach()
        kl = log_std1 - log_std0 + (std0.pow(2) + (mean0 - mean1).pow(2)) / (2.0 * std1.pow(2)) - 0.5
        return kl.sum(1, keepdim=True)

    def get_log_prob(self, x, actions):
        action_mean, action_log_std, action_std = self.forward(x)
        return normal_log_density(actions, action_mean, action_log_std, action_std)

    def get_fim(self, x):
        mean, _, _ = self.forward(x)
        cov_inv = self.action_log_std.exp().pow(-2).squeeze(0).repeat(x.size(0))
        param_count = 0
        std_index = 0
        id = 0
        for name, param in self.named_parameters():
            if name == "action_log_std":
                std_id = id
                std_index = param_count
            param_count += param.view(-1).shape[0]
            id += 1
        return cov_inv.detach(), mean, {'std_id': std_id, 'std_index': std_index}


