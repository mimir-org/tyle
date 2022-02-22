import { BadRequestData, HttpResponse } from "./index";
import { BadRequestDataItem } from "./Types";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const GetBadResponseData = (response: HttpResponse<any>): BadRequestData | undefined => {
  if (response.status !== 400) return undefined;

  const title = response?.data?.title ?? response?.statusText ?? "";

  const data: BadRequestData = {
    title: title,
    items: [],
  };

  for (const [key, value] of Object.entries(response.data)) {
    const item = { key, value } as BadRequestDataItem;
    data.items.push(item);
  }

  return data;
};

export default GetBadResponseData;
