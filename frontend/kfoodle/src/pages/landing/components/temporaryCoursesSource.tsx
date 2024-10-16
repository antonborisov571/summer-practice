import { CourseCard } from "../landing.tsx";

const plainCourse: CourseCard = {
  id: "1",
  pictureHref:
    "https://intelligentemployment.com/wp-content/uploads/2021/12/AdobeStock_387227165-scaled.jpeg",
  title: "Создание голограмм на ноутбуке",
  category: {
    id: "1",
    name: "Категория",
  },
  author: {
    name: "Автор курса",
  },
  price: 7777,
  isGlowing: false,
  isOwned: true,
};
const freeCourse: CourseCard = {
  id: "1",
  pictureHref:
    "https://intelligentemployment.com/wp-content/uploads/2021/12/AdobeStock_387227165-scaled.jpeg",
  title: "Создание голограмм на ноутбуке",
  category: {
    id: "1",
    name: "Категория",
  },
  author: {
    name: "Автор курса",
  },
  price: 0,
  isGlowing: false,
  isOwned: true,
};
const courseWithSubscription: CourseCard = {
  id: "1",
  pictureHref:
    "https://intelligentemployment.com/wp-content/uploads/2021/12/AdobeStock_387227165-scaled.jpeg",
  title: "Создание голограмм на ноутбуке",
  category: {
    id: "1",
    name: "Категория",
  },
  author: {
    name: "Автор курса",
  },
  price: 77777,
  isGlowing: true,
  isOwned: false,
};

export const tempPopularCourses: Array<CourseCard> = [
  ...Array.from({ length: 2 }, () => ({ ...freeCourse })),
  ...Array.from({ length: 2 }, () => ({ ...courseWithSubscription })),
];

export const tempRecommendedCourses: Array<CourseCard> = [
  ...Array.from({ length: 50 }, () => ({ ...courseWithSubscription })),
];
