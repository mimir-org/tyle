import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "complib/assets";
import { AspectObject } from "features/common/aspectobject/AspectObject";

export default {
  title: "Features/Common/AspectObject/AspectObject",
  component: AspectObject,
} as ComponentMeta<typeof AspectObject>;

const Template: ComponentStory<typeof AspectObject> = (args) => <AspectObject {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "AspectObject",
  color: "#fef445",
  img: LibraryIcon,
};
