import { Category } from "./category";

export interface ListApps {
    appId: string,
    name: string,
    logo: string,
    description: string,
    rating: number,
    downloads: number,
    category: Category
}
