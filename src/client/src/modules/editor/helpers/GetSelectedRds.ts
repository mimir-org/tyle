import { CreateLibraryType, Rds } from "../../../models";

const GetSelectedRds = (createLibraryType: CreateLibraryType, rds: Rds[]): Rds | undefined => {
  return rds.find((r) => r.id === createLibraryType?.rdsId);
};

export default GetSelectedRds;
