import { faker } from "@faker-js/faker";
import { State } from "@mimirorg/typelibrary-types";
import { AspectObjectItem } from "common/types/aspectObjectItem";
import { mockInfoItem } from "common/utils/mocks/mockInfoItem";
import { mockAspectObjectTerminalItem } from "common/utils/mocks/mockAspectObjectTerminalItem";
import { LibraryIcon } from "complib/assets";

export const mockAspectObjectItem = (): AspectObjectItem => ({
  id: faker.string.alphanumeric(36),
  name: faker.commerce.productName(),
  description: faker.commerce.productDescription(),
  terminals: [...Array(7)].map((_) => mockAspectObjectTerminalItem()),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  img: LibraryIcon,
  kind: "AspectObjectItem",
  state: State.Draft,
  companyId: 1,
});
