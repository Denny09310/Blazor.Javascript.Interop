export interface DotNetObjectReference {
    invokeMethodAsync(identifier: string, ...args: any[]): Promise<any>
}