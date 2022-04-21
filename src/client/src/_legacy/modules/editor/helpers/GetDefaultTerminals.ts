import { CreateLibraryType, TerminalTypeItem } from "../../../models";

const GetDefaultTerminals = (categoryId: string, createLibraryType: CreateLibraryType): TerminalTypeItem[] => {
  return createLibraryType.terminalTypes?.filter((x) => x.categoryId === categoryId);
};
export default GetDefaultTerminals;
