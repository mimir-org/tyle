import { faker } from "@faker-js/faker";
import { LibraryIcon } from "complib/assets";
import { NodeItem } from "../../types/nodeItem";
import { mockInfoItem } from "./mockInfoItem";
import { mockNodeTerminalItem } from "./mockNodeTerminalItem";

export const mockNodeItem = (): NodeItem => ({
  id: faker.random.numeric(),
  name: faker.commerce.productName(),
  description: faker.commerce.productDescription(),
  terminals: [...Array(7)].map((_) => mockNodeTerminalItem()),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  img: LibraryIcon,
  kind: "NodeItem",
});
