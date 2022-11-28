import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalCollection } from "features/common/terminal/TerminalCollection";

export default {
  title: "Features/Common/Terminal/TerminalCollection",
  component: TerminalCollection,
  args: {
    terminals: [
      { name: "Example A", color: "grey", maxQuantity: 1, direction: "Input" },
      { name: "Example B", color: "red", maxQuantity: 2, direction: "Output" },
      { name: "Example C", color: "orange", maxQuantity: 3, direction: "Bidirectional" },
    ],
  },
} as ComponentMeta<typeof TerminalCollection>;

const Template: ComponentStory<typeof TerminalCollection> = (args) => <TerminalCollection {...args} />;

export const Default = Template.bind({});

export const VariantLeft = Template.bind({});
VariantLeft.args = {
  placement: "left",
};

export const VariantRight = Template.bind({});
VariantRight.args = {
  placement: "right",
};
