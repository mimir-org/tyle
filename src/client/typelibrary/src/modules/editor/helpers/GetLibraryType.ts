import { LibraryFilter, ObjectType } from "../../../models";

const GetLibraryType = (libraryType: ObjectType) => {
  switch (libraryType) {
    case ObjectType.ObjectBlock:
      return LibraryFilter.Node;
    case ObjectType.Transport:
      return LibraryFilter.Transport;
    case ObjectType.Interface:
      return LibraryFilter.Interface;
    default:
      return null;
  }
};

export default GetLibraryType;
