import { faker } from "@faker-js/faker";
import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { mockInfoItem } from "common/utils/mocks/mockInfoItem";

export const mockAspectObjectTerminalItem = (): AspectObjectTerminalItem => ({
  id: faker.string.alpha(20),
  name: `Terminal ${faker.string.alpha({ length: 3, casing: "upper" })}`,
  color: faker.internet.color(),
  maxQuantity: parseInt(faker.random.numeric(1)),
  direction: faker.helpers.arrayElement(["Input", "Output", "Bidirectional"]),
  attributes: [...Array(7)].map((_) => mockInfoItem()),
});
