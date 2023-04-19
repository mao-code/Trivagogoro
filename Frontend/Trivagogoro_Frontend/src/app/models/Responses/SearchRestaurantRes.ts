export interface SearchRestaurantRes
{
  id: number,
  name: string,
  lat: number,
  lng: number,
  address: string,
  placeId: string,
  priceLevel: number,
  images: string[],
}
