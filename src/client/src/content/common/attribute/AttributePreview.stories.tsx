import { faker } from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AttributePreview } from "./AttributePreview";

export default {
  title: "Content/Common/Attribute/AttributePreview",
  component: AttributePreview,
} as ComponentMeta<typeof AttributePreview>;

const Template: ComponentStory<typeof AttributePreview> = (args) => <AttributePreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Attribute",
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  quantityDatumSpecifiedScope: "Design Datum",
  quantityDatumSpecifiedProvenance: "Measured Datum",
  quantityDatumRangeSpecifying: "Nominal Datum",
  quantityDatumRegularitySpecified: "Absolute Datum",
  contents: [
    {
      name: "Values",
      descriptors: {
        1: "A",
        2: "B",
        3: "C",
      },
    },
    {
      name: "References",
      descriptors: {
        1: "PCA",
        2: "BBB",
        3: "CCC",
      },
    },
  ],
};
