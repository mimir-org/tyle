import { attributeFormBasePath } from "components/AttributeForm/AttributeFormRoutes";
import { attributeGroupFormBasePath } from "components/AttributeGroupForm/AttributeGroupFormRoutes";
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
    {
      name: "Attribute Group",
      path: attributeGroupFormBasePath,
    },
  ];
};

export const isPositiveInt = (value: string | null): boolean => {
  return value ? /^\d+$/.test(value) && Number(value) > 0 : false;
};
