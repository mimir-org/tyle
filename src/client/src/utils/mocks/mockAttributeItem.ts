import { faker } from "@faker-js/faker";
import { AttributeItem } from "../../content/home/types/AttributeItem";

export const mockAttributeItem = (): AttributeItem => {
  return {
    id: faker.random.alphaNumeric(),
    name: `${faker.commerce.productAdjective()}`,
    traits: mockAttributeItemTraits(parseInt(faker.random.numeric(1))),
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
