import numpy as np
from scipy import signal
from PIL import Image

class GaussianBlur(object):
    def __init__(self, kernel_size=3, sigma=1.1):
        self.kernel_size = kernel_size
        self.sigma = sigma
        self.kernel =self.gaussian_kernel()
    def gaussian_kernel(self):
        kernel = np.zeros(shape=(self.kernel_size,self.kernel_size),dtype=float)
        radius = self.kernel_size//2
        for y in range(-radius, radius+1):
            for x in range(-radius, radius):
                v = 1.0 / (2 * np.pi* self.sigma **2) * np.exp(-1.0 / (2 * self.sigma ** 2)* (x **2 + y **2))
                kernel[y + radius, x + radius] = v
        kernel2 = kernel / np.sum(kernel)

        return kernel2
    def filter(self, img):
        img_arr = np.array(img)
        if len(img_arr.shape) == 2:
            new_arr = signal.convolve2d(img_arr, self.kernel, mode = "same", boundary="symm")
        else:
            h,w,c = img_arr.shape
            new_arr = np.zeros(shape=(h,w,c),dtype=float)
            for i in range(c):
                new_arr[..., i]  = signal.convolve2d(img_arr[...,i], self.kernel, mode ="same", boundary='symm')
        new_arr = np.array(new_arr, dtype=np.uint8)
        return Image.fromarray(new_arr)
def concat(image, post_image,image_mask):

    threshold = 128
    binary_mask = image_mask.point(lambda p: p > threshold and 255) 
    result = Image.composite(image, post_image, binary_mask)
    return result

def pipeline(img_path, mask_path):
    img = Image.open(img_path).convert('RGB')
    mask = Image.open(mask_path).convert('L')
    img2 = GaussianBlur(sigma=1).filter(img)
    final_img = concat(img, img2, mask)
    return final_img


if __name__=='__main__':
    img_path = 'test/img.png'
    mask_path = 'test/mask.png'
    final_img = pipeline(img_path,mask_path)
    final_img.save('test/result.png')