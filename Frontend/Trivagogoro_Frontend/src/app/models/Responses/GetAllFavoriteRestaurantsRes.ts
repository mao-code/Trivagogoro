export interface GetAllFavoriteRestaurantsRes
{
  favoriteRestaurants: GetAllFavoriteRestaurantsDTO[]
}

export interface GetAllFavoriteRestaurantsDTO
{
  favId: number,
  selfRating: number,
  resId: number,
  resName: string
  placeId: string,
  images: string[]
}
