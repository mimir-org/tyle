import { faker } from "@faker-js/faker";
import { AttributeItem } from "../../content/types/AttributeItem";
import { mockInfoItem } from "./mockInfoItem";

export const mockAttributeItem = (): AttributeItem => ({
  id: faker.random.numeric(),
  name: faker.commerce.productName(),
  description: faker.commerce.productDescription(),
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  qualifier: faker.word.adjective(),
  source: faker.word.adjective(),
  condition: faker.word.adjective(),
  tokens: [...Array(5)].map((_) => faker.commerce.productAdjective()),
  contents: [...Array(2)].map((_) => mockInfoItem()),
  kind: "AttributeItem",
});
