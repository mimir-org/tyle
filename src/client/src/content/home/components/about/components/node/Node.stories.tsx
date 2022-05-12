import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Node } from "./Node";

export default {
  title: "Content/Home/About/Node/Node",
  component: Node,
  args: {
    color: "#fef445",
    img: "static/media/src/assets/icons/modules/library.svg",
  },
} as ComponentMeta<typeof Node>;

const Template: ComponentStory<typeof Node> = (args) => <Node {...args} />;

export const Default = Template.bind({});
