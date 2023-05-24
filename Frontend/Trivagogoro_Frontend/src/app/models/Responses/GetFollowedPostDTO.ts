export interface GetFollowPostsRes
{
  followedPostDTOs: GetFollowPostsDTO[]
}

export interface GetFollowPostsDTO
{
  flId: number,
  flName: string,
  fwpId: number,
  fwpDescription: string
  fwpTitle: string,
  fwpArchivedNum: number
  fwpType: number,
  fwpSourceId: number,
  placeId: string,
  images: string[]
}
