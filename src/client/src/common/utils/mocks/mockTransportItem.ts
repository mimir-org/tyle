import { faker } from "@faker-js/faker";
import { State } from "@mimirorg/typelibrary-types";
import { TransportItem } from "common/types/transportItem";
import { mockInfoItem } from "common/utils/mocks/mockInfoItem";
import { mockTerminalItem } from "common/utils/mocks/mockTerminalItem";

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
  state: State.Draft,
  companyId: 1
});
