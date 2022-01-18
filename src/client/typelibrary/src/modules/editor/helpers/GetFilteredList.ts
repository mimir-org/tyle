/* eslint-disable @typescript-eslint/no-explicit-any */
import { CreateLibraryType } from "../../../models";
import { ListType } from "../TypeEditorList";
import { GetAttributesList, GetFilteredAttributesList, GetFilteredRdsList, GetFilteredTerminalsList } from "./index";

const GetFilteredList = (
  listType: ListType,
  items: any,
  createLibraryType: CreateLibraryType
  // discipline?: Discipline
): any[] => {
  const aspect = createLibraryType?.aspect;
  switch (listType) {
    case ListType.Rds:
      return GetFilteredRdsList(items, aspect);
    case ListType.Terminals:
      return GetFilteredTerminalsList(items);
    case ListType.PredefinedAttributes:
      return items;
    case ListType.ObjectAttributes:
      return GetFilteredAttributesList(items, aspect);
    case ListType.LocationAttributes:
      return GetAttributesList(items, aspect);
    case ListType.SimpleTypes:
      return items;
    default:
      return [] as any[];
  }
};

export default GetFilteredList;
