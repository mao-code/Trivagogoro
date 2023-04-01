export interface ResponseData<T>
{
  code: number,
  isSuccess: boolean,
  message: string,
  data?: T
}
