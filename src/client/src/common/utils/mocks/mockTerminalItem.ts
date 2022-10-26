import { faker } from "@faker-js/faker";
import { TerminalItem } from "../../types/terminalItem";
import { mockInfoItem } from "./mockInfoItem";

export const mockTerminalItem = (): TerminalItem => ({
  id: faker.random.alphaNumeric(),
  name: `Terminal ${faker.random.alpha({ count: 3, casing: "upper" })}`,
  color: faker.internet.color(),
  description: faker.commerce.productDescription(),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  kind: "TerminalItem",
});
