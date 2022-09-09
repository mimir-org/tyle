import { InterfaceItem } from "../../content/types/InterfaceItem";

export const isInterfaceItem = (item: unknown): item is InterfaceItem => (<InterfaceItem>item).kind === "InterfaceItem";
