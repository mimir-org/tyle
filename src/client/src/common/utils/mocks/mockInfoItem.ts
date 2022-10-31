import { faker } from "@faker-js/faker";
import { InfoItem } from "common/types/infoItem";

export const mockInfoItem = (): InfoItem => {
  return {
    id: faker.random.alphaNumeric(),
    name: `${faker.commerce.productAdjective()}`,
    descriptors: mockInfoItemDescriptors(parseInt(faker.random.numeric(1))),
  };
};

const mockInfoItemDescriptors = (amount = 5) => {
  const traits: { [key: string]: string } = {};

  for (let i = 0; i < amount; i++) {
    const name = faker.commerce.product();
    traits[name] = faker.commerce.productAdjective();
  }

  return traits;
};
