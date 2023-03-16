import { faker } from "@faker-js/faker";
import { State } from "@mimirorg/typelibrary-types";
import { NodeItem } from "common/types/nodeItem";
import { mockInfoItem } from "common/utils/mocks/mockInfoItem";
import { mockNodeTerminalItem } from "common/utils/mocks/mockNodeTerminalItem";
import { LibraryIcon } from "complib/assets";

export const mockNodeItem = (): NodeItem => ({
  id: Number.parseInt(faker.random.numeric()),
  name: faker.commerce.productName(),
  description: faker.commerce.productDescription(),
  terminals: [...Array(7)].map((_) => mockNodeTerminalItem()),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  img: LibraryIcon,
  kind: "NodeItem",
  state: State.Draft,
  companyId: 1,
});
