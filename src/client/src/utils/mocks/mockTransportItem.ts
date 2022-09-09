import { faker } from "@faker-js/faker";
import { TransportItem } from "../../content/types/TransportItem";
import { mockInfoItem } from "./mockInfoItem";
import { mockTerminalItem } from "./mockTerminalItem";

export const mockTransportItem = (): TransportItem => ({
  id: faker.random.alphaNumeric(),
  name: `Transport ${faker.random.alpha({ count: 3, casing: "upper" })}`,
  aspectColor: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  transportColor: faker.internet.color(),
  description: faker.commerce.productDescription(),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
  terminal: mockTerminalItem(),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  kind: "TransportItem",
});
