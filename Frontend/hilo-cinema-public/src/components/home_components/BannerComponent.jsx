<<<<<<< HEAD
﻿import { Carousel } from '@material-tailwind/react';

export function CarouselDefault() {
  //const carouselImages = useSelector((state) => state.home.carouselImages);
  var carouselImages = [
    'https://cdn.galaxycine.vn/media/2024/8/8/lam-giau-voi-ma-2048_1723108931285.jpg',
    'https://cdn.galaxycine.vn/media/2024/8/16/ma-da-2048_1723799769236.jpg',
    'https://cdn.galaxycine.vn/media/2024/8/12/harold-2048_1723454887262.jpg',
    'https://cdn.galaxycine.vn/media/2024/7/31/shopee-x-glx-thang-8-1_1722438044809.jpg'
  ]
  
=======
﻿import { useSelector } from 'react-redux';
import { Carousel } from '@material-tailwind/react';

export function CarouselDefault() {
  const carouselImages = useSelector((state) => state.home.carouselImages);

>>>>>>> 960a83c (commit)
  return (
    <Carousel className="rounded-xl">
      {carouselImages.map((image, index) => (
        <img
          key={index}
<<<<<<< HEAD
          src={image}
=======
          src={image.src}
>>>>>>> 960a83c (commit)
          alt={`image ${index + 1}`}
          className="h-full w-full object-cover"
        />
      ))}
    </Carousel>
  );
}
