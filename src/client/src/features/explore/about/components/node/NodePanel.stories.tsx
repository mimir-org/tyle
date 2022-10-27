import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockNodeItem } from "common/utils/mocks";
import { NodePanel } from "./NodePanel";

export default {
  title: "Explore/About/NodePanel",
  component: NodePanel,
  args: {
    ...mockNodeItem(),
  },
} as ComponentMeta<typeof NodePanel>;

const Template: ComponentStory<typeof NodePanel> = (args) => <NodePanel {...args} />;

export const Default = Template.bind({});
