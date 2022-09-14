import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockTerminalItem } from "../../../../../../utils/mocks/mockTerminalItem";
import { TerminalPanel } from "./TerminalPanel";

export default {
  title: "Content/Explore/About/TerminalPanel",
  component: TerminalPanel,
  args: {
    ...mockTerminalItem(),
  },
} as ComponentMeta<typeof TerminalPanel>;

const Template: ComponentStory<typeof TerminalPanel> = (args) => <TerminalPanel {...args} />;

export const Default = Template.bind({});
