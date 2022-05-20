import { ComponentMeta, ComponentStory } from "@storybook/react";
import { NodePanel } from "./NodePanel";
import { mockNodeItem } from "../../../../../../utils/mocks";

export default {
  title: "Content/Home/About/Panel/NodePanel",
  component: NodePanel,
  args: {
    ...mockNodeItem(),
  },
} as ComponentMeta<typeof NodePanel>;

const Template: ComponentStory<typeof NodePanel> = (args) => <NodePanel {...args} />;

export const Default = Template.bind({});
