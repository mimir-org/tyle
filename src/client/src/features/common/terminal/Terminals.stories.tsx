import { Meta, StoryFn } from "@storybook/react";
import { Terminals } from "features/common/terminal/Terminals";

export default {
  title: "Features/Common/Terminal/Terminals",
  component: Terminals,
  args: {
    showCollectionLimit: 5,
    terminals: [
      { name: "Example A", color: "grey", maxQuantity: 1, direction: "Input" },
      { name: "Example B", color: "red", maxQuantity: 2, direction: "Input" },
      { name: "Example C", color: "orange", maxQuantity: 3, direction: "Input" },
      { name: "Example D", color: "purple", maxQuantity: 4, direction: "Bidirectional" },
    ],
  },
} as Meta<typeof Terminals>;

const Template: StoryFn<typeof Terminals> = (args) => <Terminals {...args} />;

export const CollectionLimitBelow = Template.bind({});
CollectionLimitBelow.args = {
  terminals: [
    { name: "Example A", color: "grey", maxQuantity: 1, direction: "Output" },
    { name: "Example B", color: "red", maxQuantity: 2, direction: "Output" },
    { name: "Example C", color: "orange", maxQuantity: 3, direction: "Output" },
    { name: "Example D", color: "purple", maxQuantity: 4, direction: "Bidirectional" },
  ],
};

export const CollectionLimitAbove = Template.bind({});
CollectionLimitAbove.args = {
  terminals: [
    { name: "Example A", color: "grey", maxQuantity: 1, direction: "Input" },
    { name: "Example B", color: "red", maxQuantity: 2, direction: "Input" },
    { name: "Example C", color: "orange", maxQuantity: 3, direction: "Input" },
    { name: "Example D", color: "purple", maxQuantity: 4, direction: "Input" },
    { name: "Example E", color: "purple", maxQuantity: 4, direction: "Bidirectional" },
    { name: "Example F", color: "purple", maxQuantity: 4, direction: "Bidirectional" },
  ],
};

export const VariantLeft = Template.bind({});
VariantLeft.args = {
  ...CollectionLimitAbove.args,
  placement: "left",
};

export const VariantRight = Template.bind({});
VariantRight.args = {
  ...CollectionLimitAbove.args,
  placement: "right",
};
