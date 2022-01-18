import {
  Aspect,
  AttributeType,
  CreateLibraryType,
  ObjectType, PredefinedAttribute,
  Rds, SimpleType,
  TerminalTypeDict,
  TerminalTypeItem
} from "../../models";

export type AspectKey = keyof typeof Aspect;
export type ObjectTypeKey = keyof typeof ObjectType;
export type TerminalCategoryChangeKey = "add" | "remove" | "update" | "removeAll" | "terminalTypeId";
export type OnPropertyChangeFunction = <K extends keyof CreateLibraryType>(key: K, value: CreateLibraryType[K]) => void;
export type OnTerminalCategoryChangeFunction = <K extends TerminalCategoryChangeKey>(key: K, value: TerminalTypeItem) => void;

/**
 * TODO: Refactor the use of ListItemType
 * If it is used to pick a strategy (for filtering), it should be renamed to "xxxStrategy" or something similar.
 **/
export type ListItemType = Rds[] | TerminalTypeDict | AttributeType[] | SimpleType[] | PredefinedAttribute[];