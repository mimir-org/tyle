import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockNodeItem } from "common/utils/mocks";
import { NodePanel } from "features/explore/about/components/node/NodePanel";

export default {
  title: "Features/Explore/About/NodePanel",
  component: NodePanel,
  args: {
    ...mockNodeItem(),
  },
} as ComponentMeta<typeof NodePanel>;

const Template: ComponentStory<typeof NodePanel> = (args) => <NodePanel {...args} />;

export const Default = Template.bind({});
