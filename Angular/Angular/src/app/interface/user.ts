import { Guid } from "guid-typescript";

export interface AllAppsInfo
{
    appId : Guid;
    name : string;
    logo : Uint8Array;
    categoryName : string;
    rating : number;
    downloads : number;
    description : string;
    categoryId : Guid;
    apps : number;
    userId : Guid;
    commands : string;
    status : RequestStatus;
    }

    export enum RequestStatus
    {
        Declined = -1,
        Pending = 0,
        Approved = 1
    }

export interface SpecificAppInfo
{
    appId : Guid;
    name : string;
    description : string;
    logo : any;
    userId : Guid;
    appImages : [];
    apps : number;
    rating : number;
    commands : [];
    categoryName : string;
    categoryId : Guid
    downloads : number;
    publisherName : string;
    publishedDate : string;
}

export interface AppReviewsInfo
{
    appId: Guid;
    // commands : {
    //     [key : string ] : string[]
    // };
    commands : [];
    appCount : number;
    username : [];
    averageRatings : number;
}

export interface DownloadedAppsInfo
{
    fileid : Guid;
    fileName : string;
    logo : Uint8Array;
    description : string;
    downloads : number;
    apps : number;
    rating : number;
    category : string;
}


export interface DeveloperAppInfo
{
    appId : Guid;
    name : string;
    logo : Uint8Array;
    categoryName : string;
    rating : number;
    downloads : number;
    description : string;
    apps : number;
    commands : string;
    publishedDate : Date;
    PublisherName : string;
    status : RequestStatus
}

export interface CategoryInfo
{
    categoryName : string;
    categoryId : Guid;
}