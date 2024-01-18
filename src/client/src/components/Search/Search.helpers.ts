import { attributeFormBasePath } from "components/AttributeForm/AttributeFormRoutes";
import { blockFormBasePath } from "components/BlockForm/BlockFormRoutes";
import { terminalFormBasePath } from "components/TerminalForm/TerminalFormRoutes";
import { Link } from "types/link";

export const useCreateMenuLinks = (): Link[] => {
  return [
    {
      name: "Block",
      path: blockFormBasePath,
    },
    {
      name: "Terminal",
      path: terminalFormBasePath,
    },
    {
      name: "Attribute",
      path: attributeFormBasePath,
    },
  ];
};

export const isPositiveInt = (value: string | null): boolean => {
  return value ? /^\d+$/.test(value) && Number(value) > 0 : false;
};
