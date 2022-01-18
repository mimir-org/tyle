/* eslint-disable @typescript-eslint/no-explicit-any */
import { Aspect } from "../../../models";

const GetFilteredRdsList = (rds: any[], aspect: Aspect): any[] => {
  if (!rds || rds.length <= 0) return [] as any[];

  const rdsByAspect = rds.filter((x) => aspect & x.aspect);
  const rdsByCategories = [];
  const categoriesArray = [...new Set(rds.map((r) => r.rdsCategory.name))];

  categoriesArray.forEach((category) => {
    const cat = { name: category, items: [] };

    rdsByAspect.forEach((r) => {
      if (r.rdsCategory.name === category) cat.items.push(r);
    });
    if (cat.items.length > 0) rdsByCategories.push(cat);
  });
  return rdsByCategories;
};

export default GetFilteredRdsList;
