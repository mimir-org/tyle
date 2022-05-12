import faker from "@faker-js/faker";
import { NodeItem } from "../../content/home/types/NodeItem";
import { mockTerminalItem } from "./mockTerminalItem";

export const mockNodeItem = (): NodeItem => ({
  name: faker.commerce.productName(),
  description: faker.commerce.productDescription(),
  terminals: [...Array(7)].map((_) => mockTerminalItem()),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  img: "static/media/src/assets/icons/modules/library.svg",
});
