import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Processing } from "./Processing";

export default {
  title: "Content/Forms/Auth/Common/Processing",
  component: Processing,
} as ComponentMeta<typeof Processing>;

const Template: ComponentStory<typeof Processing> = (args) => <Processing {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "Now loading",
};
