import { Guid } from "guid-typescript"
import { AppImages } from "./app-images"
import { Category } from "./category"
import { RequestedUser } from "./requested-user"

export interface RequestDetails {
    appId: string,
    name: string,
    description: string,
    logo: string,
    status: number,
    users: RequestedUser,
    category: Category,
    publisherName: string,
    appImages: AppImages[]
}
