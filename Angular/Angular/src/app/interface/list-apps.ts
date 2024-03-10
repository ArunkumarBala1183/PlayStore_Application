import { Guid } from "guid-typescript";
import { Category } from "./category";

export interface ListApps {
    appId: Guid,
    name: string,
    logo: string,
    description: string,
    rating: number,
    downloads: number,
    category: Category,
    userDownloaded: boolean
}
