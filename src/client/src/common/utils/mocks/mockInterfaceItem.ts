import { faker } from "@faker-js/faker";
import { State } from "@mimirorg/typelibrary-types";
import { InterfaceItem } from "common/types/interfaceItem";
import { mockInfoItem } from "common/utils/mocks/mockInfoItem";
import { mockTerminalItem } from "common/utils/mocks/mockTerminalItem";

export const mockInterfaceItem = (): InterfaceItem => ({
  id: faker.random.alphaNumeric(),
  name: `Interface ${faker.random.alpha({ count: 3, casing: "upper" })}`,
  aspectColor: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  interfaceColor: faker.internet.color(),
  description: faker.commerce.productDescription(),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
  terminal: mockTerminalItem(),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  kind: "InterfaceItem",
  state: State.Draft,
  companyId: 1,
});
