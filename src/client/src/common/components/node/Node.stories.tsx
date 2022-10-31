import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Node } from "common/components/node/Node";
import { LibraryIcon } from "complib/assets";

export default {
  title: "Common/Node/Node",
  component: Node,
} as ComponentMeta<typeof Node>;

const Template: ComponentStory<typeof Node> = (args) => <Node {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Node",
  color: "#fef445",
  img: LibraryIcon,
};
