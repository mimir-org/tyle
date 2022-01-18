import { BlobData, CreateLibraryType } from "../../../models";

const GetSelectedIcon = (createLibraryType: CreateLibraryType, icon: BlobData[]): BlobData | undefined => {
  return icon.find((i) => i.id === createLibraryType?.symbolId);
};

export default GetSelectedIcon;
