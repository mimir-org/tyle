import { faker } from "@faker-js/faker";
import { State } from "@mimirorg/typelibrary-types";
import { TerminalItem } from "common/types/terminalItem";
import { mockInfoItem } from "common/utils/mocks/mockInfoItem";

export const mockTerminalItem = (): TerminalItem => ({
  id: faker.string.alphanumeric(36),
  name: `Terminal ${faker.string.alpha({ length: 3, casing: "upper" })}`,
  color: faker.internet.color(),
  description: faker.commerce.productDescription(),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  kind: "TerminalItem",
  state: State.Draft,
  createdBy: faker.string.alphanumeric(36),
});
