import { CreateLibraryType, TerminalType } from "../../../models";
import { ListType } from "../TypeEditorList";
import { ListItemType } from "../types";
import { GetFilteredTerminalsList } from "./index";

const GetDefaultTerminal = (listType: ListType, createLibraryType: CreateLibraryType, items: ListItemType): TerminalType | undefined => {
  return GetFilteredTerminalsList(items)
    .flatMap((category) => category.items)
    .find((type) => type.id === createLibraryType.terminalTypeId);
};

export default GetDefaultTerminal;
