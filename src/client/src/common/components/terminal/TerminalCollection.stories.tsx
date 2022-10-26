import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalCollection } from "./TerminalCollection";

export default {
  title: "Common/Terminal/TerminalCollection",
  component: TerminalCollection,
  args: {
    terminals: [
      { name: "Example A", color: "grey", amount: 1, direction: "Input" },
      { name: "Example B", color: "red", amount: 2, direction: "Output" },
      { name: "Example C", color: "orange", amount: 3, direction: "Bidirectional" },
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
