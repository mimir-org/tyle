import { faker } from "@faker-js/faker";
import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { mockInfoItem } from "common/utils/mocks/mockInfoItem";

export const mockAspectObjectTerminalItem = (): AspectObjectTerminalItem => ({
  name: `Terminal ${faker.random.alpha({ count: 3, casing: "upper" })}`,
  color: faker.internet.color(),
  maxQuantity: parseInt(faker.random.numeric(1)),
  direction: faker.helpers.arrayElement(["Input", "Output", "Bidirectional"]),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
});
