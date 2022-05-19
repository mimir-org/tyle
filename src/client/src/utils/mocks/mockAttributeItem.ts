import { AttributeItem } from "../../content/home/types/AttributeItem";
import faker from "@faker-js/faker";

export const mockAttributeItem = (): AttributeItem => {
  return {
    name: `${faker.commerce.productAdjective()}`,
    color: faker.internet.color(),
    traits: mockAttributeItemTraits(parseInt(faker.random.numeric(1))),
    value: `${faker.random.numeric(2)} ${faker.helpers.arrayElement(["%", "bbl/d", "Sm3/h"])}`,
  };
};

const mockAttributeItemTraits = (amount = 5) => {
  const traits: { [key: string]: string } = {};

  for (let i = 0; i < amount; i++) {
    const name = faker.commerce.product();
    traits[name] = faker.commerce.productAdjective();
  }

  return traits;
};
