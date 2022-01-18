/* eslint-disable @typescript-eslint/no-explicit-any */
import { Aspect } from "../../../models";

const GetAttributesList = (attributes: any[], aspect: Aspect): any[] => {
  if (!attributes || attributes.length <= 0) return [] as any[];
  return attributes.filter((x) => aspect & x.aspect);
};

export default GetAttributesList;
