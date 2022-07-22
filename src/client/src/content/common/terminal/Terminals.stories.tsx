import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Terminals } from "./Terminals";

export default {
  title: "Content/Common/Terminal/Terminals",
  component: Terminals,
  args: {
    showCollectionLimit: 5,
    terminals: [
      { name: "Example A", color: "grey", amount: 1, direction: "Input" },
      { name: "Example B", color: "red", amount: 2, direction: "Input" },
      { name: "Example C", color: "orange", amount: 3, direction: "Input" },
      { name: "Example D", color: "purple", amount: 4, direction: "Bidirectional" },
    ],
  },
} as ComponentMeta<typeof Terminals>;

const Template: ComponentStory<typeof Terminals> = (args) => <Terminals {...args} />;

export const CollectionLimitBelow = Template.bind({});
CollectionLimitBelow.args = {
  terminals: [
    { name: "Example A", color: "grey", amount: 1, direction: "Output" },
    { name: "Example B", color: "red", amount: 2, direction: "Output" },
    { name: "Example C", color: "orange", amount: 3, direction: "Output" },
    { name: "Example D", color: "purple", amount: 4, direction: "Bidirectional" },
  ],
};

export const CollectionLimitAbove = Template.bind({});
CollectionLimitAbove.args = {
  terminals: [
    { name: "Example A", color: "grey", amount: 1, direction: "Input" },
    { name: "Example B", color: "red", amount: 2, direction: "Input" },
    { name: "Example C", color: "orange", amount: 3, direction: "Input" },
    { name: "Example D", color: "purple", amount: 4, direction: "Input" },
    { name: "Example E", color: "purple", amount: 4, direction: "Bidirectional" },
    { name: "Example F", color: "purple", amount: 4, direction: "Bidirectional" },
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
