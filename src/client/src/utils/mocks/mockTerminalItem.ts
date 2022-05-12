import faker from "@faker-js/faker";
import { TerminalItem } from "../../content/home/types/TerminalItem";

export const mockTerminalItem = (): TerminalItem => ({
  name: `Terminal ${faker.random.alpha({ count: 3, upcase: true })}`,
  color: faker.internet.color(),
  amount: parseInt(faker.random.numeric(1)),
  direction: faker.helpers.arrayElement(["Input", "Output", "Bidirectional"]),
});
