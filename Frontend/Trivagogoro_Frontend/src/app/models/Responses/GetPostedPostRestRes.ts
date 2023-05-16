export interface GetPostedPostRestRes
{
  postedPostRest: GetPostedPostRestDTO[]
}

export interface GetPostedPostRestDTO
{
  postId: number,
  sourceId: number,
  title: string,
  description: string,
  archivedNum: number,
  favId: number,
  restId: number,
  placeId: string,
  images: string[]
}
