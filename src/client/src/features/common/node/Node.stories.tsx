import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "complib/assets";
import { Node } from "features/common/node/Node";

export default {
  title: "Features/Common/Node/Node",
  component: Node,
} as ComponentMeta<typeof Node>;

const Template: ComponentStory<typeof Node> = (args) => <Node {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Node",
  color: "#fef445",
  img: LibraryIcon,
};
