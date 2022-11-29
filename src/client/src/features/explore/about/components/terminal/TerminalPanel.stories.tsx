import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockTerminalItem } from "common/utils/mocks/mockTerminalItem";
import { TerminalPanel } from "features/explore/about/components/terminal/TerminalPanel";

export default {
  title: "Features/Explore/About/TerminalPanel",
  component: TerminalPanel,
  args: {
    ...mockTerminalItem(),
  },
} as ComponentMeta<typeof TerminalPanel>;

const Template: ComponentStory<typeof TerminalPanel> = (args) => <TerminalPanel {...args} />;

export const Default = Template.bind({});
